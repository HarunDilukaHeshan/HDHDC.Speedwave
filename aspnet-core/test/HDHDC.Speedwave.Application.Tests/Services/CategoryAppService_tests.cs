using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace HDHDC.Speedwave.Services
{
    public class CategoryAppService_tests : SpeedwaveApplicationTestBase
    {
        protected ICategoryAppService CategoryAppService { get; }
        protected TestDataProvider TestDataProvider { get; }

        public CategoryAppService_tests()
        {
            CategoryAppService = GetRequiredService<ICategoryAppService>();
            TestDataProvider = GetRequiredService<TestDataProvider>();
        }

        [Fact]
        public async Task Should_Able_To_Create_A_Category()
        {
            
            var categoryCreateDto = new CategoryCreateDto
            {
                CategoryName = "Corn bread yummy",
                CategoryDescription = "Corn break from sri lankan farms",
                ThumbnailBase64 = Convert.ToBase64String(TestDataProvider.GetPicture())
            };

            var categoryDto = await CategoryAppService.CreateAsync(categoryCreateDto);

            categoryDto.ShouldNotBeNull();

            categoryDto.Id.ShouldBeGreaterThan(0);
            categoryDto.CategoryName.ShouldBe(categoryCreateDto.CategoryName);
            categoryDto.CategoryDescription.ShouldBe(categoryCreateDto.CategoryDescription);
            categoryDto.CategoryThumbnail.ShouldNotBeNullOrEmpty();
            categoryDto.CategoryThumbnail.ShouldNotBeNullOrWhiteSpace();
        }
    }
}
