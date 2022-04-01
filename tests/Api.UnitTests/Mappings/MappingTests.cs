using AutoMapper;
using Api.ApplicationCore.DTOs;
using Api.ApplicationCore.Entities;
using Api.ApplicationCore.Mappings;
using Xunit;
using System;
using System.Runtime.Serialization;

namespace Api.UnitTests.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GeneralProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldBeValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
        
        [Theory]
        [InlineData(typeof(CreateBookRequest), typeof(Book))]
        [InlineData(typeof(Book), typeof(BookResponse))]
        public void Map_SourceToDestination_ExistConfiguration(Type origin, Type destination)
        {
            var instance = FormatterServices.GetUninitializedObject(origin);

            _mapper.Map(instance, origin, destination);
        }
    }
}
