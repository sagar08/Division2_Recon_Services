using AutoMapper;
using Division2ReconService.Controllers;
using Division2ReconService.Data;
using Division2ReconService.Infrastructure;
using Division2ReconService.Models;
using Division2ReconService.Profiles;
using Division2ReconService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Division2ReconService.Test;

/// <summary>
/// 
/// </summary>
public class CustomerControllerTest
{

    private readonly CustomersController _controller;
    private readonly ICustomerService _service;

    public CustomerControllerTest()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var configuration = new ConfigurationBuilder()
           .SetBasePath(currentDirectory)
            .AddJsonFile(@"appsettings.json", false, true)
            .AddEnvironmentVariables()
            .Build();

        var logger = new Mock<ILoggerManager>();

        //Configuration Automapper
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        });
        var mapper = mockMapper.CreateMapper();

        // Configure Database
        var dbFilePath = @"E:\Projects_GitHub\WassenBurg_Assignment\Division2_Recon_Services\src\Division2ReconService\Division2ReconDB.db";
        var connectionString = $"Data Source={dbFilePath};Cache=Shared";
        //var connectionString = configuration.GetConnectionString("SqliteDivision2ReconDB");
        var options = new DbContextOptionsBuilder<Division2ReconDbContext>()
            .UseSqlite(connectionString)
            .Options;

        var dbContext = new Division2ReconDbContext(options);
        _service = new CustomerService(dbContext, logger.Object, mapper);
        _controller = new CustomersController(logger.Object, _service);
    }

    [Fact]
    public void GetActiveCustomers_Returns_OkResult()
    {
        // Act
        var result = _controller.GetActiveCustomers().Result;
        // Assert
        Assert.IsType<OkObjectResult>(result as OkObjectResult);
    }

    [Fact]
    public void GetActiveCustomers_Returns_AllItems()
    {
        // Act
        var result = _controller.GetActiveCustomers().Result;
        // Assert
        var response = Assert.IsType<ResponseDto<List<CustomerResponseDto>>>(((OkObjectResult)result).Value);
        Assert.Equal(3, response.Data.Count);
    }

    [Fact]
    public void GetCustomerMachines_Returns_OkResult()
    {
        // Act
        var result = _controller.GetCustomerProcesses().Result;
        // Assert
        Assert.IsType<OkObjectResult>(result as OkObjectResult);
    }

    [Fact]
    public void GetCustomerMachines_Returns_AllItems()
    {
        // Act
        var result = _controller.GetCustomerProcesses().Result;
        // Assert
        var response = Assert.IsType<ResponseDto<List<ProcessResponseDto>>>(((OkObjectResult)result).Value);
        Assert.Equal(3, response.Data.Count);
    }

}