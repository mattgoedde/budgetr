// System
global using System;
global using System.Linq;
global using System.Net;
global using System.Collections.Generic;
global using System.Threading.Tasks;
// Microsoft
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Azure.Cosmos;
global using Microsoft.Azure.Cosmos.Linq;
global using Microsoft.Azure.WebJobs;
global using Microsoft.Azure.WebJobs.Extensions.Http;
global using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
global using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
global using Microsoft.Extensions.Logging;
global using Microsoft.OpenApi.Models;
// Budgetr
global using Budgetr.Class.Entities;
global using Budgetr.Class.ApiModels;
global using Budgetr.Functions.Services.Interfaces;
global using Budgetr.Functions.Services.Cosmos;
global using Budgetr.Logic.Extensions;
// Other
global using FluentValidation;