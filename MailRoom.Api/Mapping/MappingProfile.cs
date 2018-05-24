using AutoMapper;
using MailRoom.Api.Models;
using MailRoom.Api.ViewModels;

namespace MailRoom.Api.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Domain to Api Resources
            CreateMap<JobManifest, JobManifestResource>();
            CreateMap<JobManifestBranch, JobManifestResource>();
            CreateMap<JobManifestLog, JobManifestLogResource>();
            // Api Resources to Domain
            //CreateMap<IssueResponseSaveResource, IssueResponse>();
        
        }

    }
}