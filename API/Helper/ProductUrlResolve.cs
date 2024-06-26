﻿using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
namespace API.Helper
{
    public class ProductUrlResolve : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolve(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember,
            ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.pictureUrl))
            {
                return _config["ApiUrl"]+source.pictureUrl;
            }
            return null; 
        }
    }
}
