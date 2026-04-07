using Audit_System.Domain.Entities;
using AuditSystem.Application.Features.AuditLog.DTOs;
using AuditSystem.Application.Features.Course.DTOs;
using AuditSystem.Application.Features.Enrollments.DTOs;
using AuditSystem.Application.Features.User.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            #region User Mapping

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();

            #endregion


            #region Course Mapping
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ReverseMap();
            #endregion

            #region Enrollment Mapping
            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
                .ReverseMap();
            #endregion

            #region AuditLog  Mapping
            CreateMap<AuditLog, AuditLogDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.EntityName, opt => opt.MapFrom(src => src.EntityName))
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.EntityId))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src.Metadata))
                .ReverseMap();
            #endregion

        }
    }
}
