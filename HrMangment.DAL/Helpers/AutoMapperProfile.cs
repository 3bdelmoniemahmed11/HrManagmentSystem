using AutoMapper;
using HrManagment.DAL.Models;
using HrManagment.DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Attendance, EmpAttendanceDTO>()
                .ForMember(dest => dest.EmpName, opt => opt.MapFrom(src => src.Employee.Name))
                .ForMember(dest => dest.DeptName, opt => opt.MapFrom(src => src.Employee.Department.Name));


            CreateMap<EmpAttendanceDTO, Attendance>()
                   .ForMember(dest => dest.AttendanceTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.AttendanceTime)))
                .ForMember(dest => dest.DepartureTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.DepartureTime)));


        }
    }
    
}
