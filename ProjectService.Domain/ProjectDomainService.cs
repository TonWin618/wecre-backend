﻿using ProjectService.Domain.Entities;

namespace ProjectService.Domain;

public class ProjectDomainService
{
    private readonly IProjectRepository repository;

    public ProjectDomainService(IProjectRepository repository)
    {
        this.repository = repository;
    }
    //TODO: sure files have correct GUIDs and they are not filled in by the client.
    public async Task DeleteProject(Project project)
    {   
        if(project.FirmwareVerisions!= null)
        {
            foreach (var firmwareVersion in project.FirmwareVerisions)
            {
                foreach (var file in firmwareVersion.Files)
                {
                    //TODO: delete files by publishing domain events
                }
            }
        }
        if(project.ModelVersions!=null)
        {
            foreach (var modelVersion in project.ModelVersions)
            {
                foreach (var file in modelVersion.Files)
                {
                    //TODO: delete files by publishing domain events
                }
            }
        }
        if(project.ModelVersions!=null)
        {
            foreach (var file in project.ReadmeFiles)
            {
                //TODO: delete files by publishing domain events
            }
        }
        repository.RemoveProject(project);
        
    }

    public void DeleteProjectVersion(ProjectVersion projectVersion)
    {
        //repository.RemoveProjectVersion(projectVersion);
    }

    public void DeleteFirmwareVersion(FirmwareVersion firmwareVersion)
    {
        foreach(var file in firmwareVersion.Files)
        {
            //TODO: delete files by publishing domain events
        }
        //repository.RemoveFirmwareVersion(firmwareVersion);
    }

    public void DeleteModelVersion(ModelVersion modelVersion)
    {
        foreach (var file in modelVersion.Files)
        {
            //TODO: delete files by publishing domain events
        }
        //repository.RemoveModelVersion(modelVersion);
    }
}