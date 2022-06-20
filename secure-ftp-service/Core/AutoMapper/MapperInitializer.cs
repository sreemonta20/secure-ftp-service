﻿using AutoMapper;
using secure_ftp_service.Core.Models;
using secure_ftp_service.Core.Requests;

namespace secure_ftp_service.Core.AutoMapper
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<SftpFileDetails, SftpFileDetailsReq>().ReverseMap();
            //CreateMap<List<SftpFileDetails>, List<SftpFileDetailsReq>>().ReverseMap();
        }
    }
}
