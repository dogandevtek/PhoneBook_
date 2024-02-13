﻿using ReportService.Application.Maps;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Application {
    public static class DependencyInjection {

        public static IServiceCollection AddApplication(this IServiceCollection services) {
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
