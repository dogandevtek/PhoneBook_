﻿using ContactService.Application.Maps;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application {
    public static class DependencyInjection {

        public static IServiceCollection AddApplication(this IServiceCollection services) {
            var assembly = typeof(DependencyInjection).Assembly;
            
            services.AddValidatorsFromAssembly(assembly);

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
