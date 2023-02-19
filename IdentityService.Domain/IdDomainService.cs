﻿using Common.JWT;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace IdentityService.Domain;

public class IdDomainService
{
    private readonly IIdRepository repository;
    private readonly ITokenService tokenService;
    private readonly IOptions<JWTOptions> optJWT;
    public IdDomainService(IIdRepository repository, ITokenService tokenService, IOptions<JWTOptions> optJWT)
    {
        this.repository = repository;
        this.tokenService = tokenService;
        this.optJWT = optJWT;
    }
    public async Task<IdentityResult> SignUp(string userName,string email,string password)
    {
        User user = new(userName);
        user.Email = email;
        var result = await repository.CreateAsync(user,password);
        return result;
    }
    private async Task<SignInResult> CheckByUserNameAndPwdAsync(string userName, string password)
    {
        var user = await repository.FindByNameAsync(userName);
        if (user == null)
        {
            return SignInResult.Failed;
        }
        var result = await repository.CheckPwdAsync(user, password);
        return result;
    }
    private async Task<SignInResult> CheckByEmailAndPwdAsync(string userName, string password)
    {
        var user = await repository.FindByEmailAsync(userName);
        if (user == null)
        {
            return SignInResult.Failed;
        }
        var result = await repository.CheckPwdAsync(user, password);
        return result;
    }
    public async Task<(SignInResult result, string token)> LoginByUserNameAndPwdAsync(string userName, string password)
    {
        var checkResult = await CheckByUserNameAndPwdAsync(userName, password);
        if(checkResult.Succeeded)
        {
            var user = await repository.FindByNameAsync(userName);
            string token = await BuildTokenAsync(user!);
            return (SignInResult.Success, token);
        }
        else
        {
            return (checkResult, null)!;
        }
        
    }

    public async Task<(SignInResult result, string token)> LoginByEmailAndPwdAsync(string email, string password)
    {
        var checkResult = await CheckByEmailAndPwdAsync(email, password);
        if (checkResult.Succeeded)
        {
            var user = await repository.FindByEmailAsync(email);
            string token = await BuildTokenAsync(user!);
            return(SignInResult.Success, token);
        }
        else
        {
            return (checkResult, null)!;
        }
    }

    private async Task<string> BuildTokenAsync(User user)
    {
        var roles = await repository.GetRolesAsync(user);
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
        foreach(string role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return tokenService.BuildToken(claims, optJWT.Value);
    }
}