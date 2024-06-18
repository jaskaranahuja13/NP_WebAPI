using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace NP_WebAPI
{
    /// <summary>
    /// Configures Swagger options for the NP_WebAPI project.
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        /// <summary>
        /// Configures the SwaggerGenOptions to add a security definition for Bearer token authentication.
        /// </summary>
        /// <param name="options">The SwaggerGenOptions to configure.</param>
        public void Configure(SwaggerGenOptions options)
        {
            // Adds a security definition for Bearer token authentication
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                // Description of the security scheme, explaining how to use it
                Description = "JWT Authorization using Bearer Scheme.\r\n\r\n" +
                              "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                              "Name: Authorization\r\n" +
                              "In: header",

                // The name of the header parameter that will hold the token
                Name = "Authorization",

                // Specifies that the parameter will be in the header
                In = ParameterLocation.Header,

                // Specifies the type of security scheme
                Type = SecuritySchemeType.ApiKey,

                // The scheme name used for the bearer token
                Scheme = "Bearer"
            });

            // Adds a security requirement for the Bearer token authentication
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    // Defines the security scheme to be used
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        }
    }
}
