using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;
using amorphie.resource.data;

namespace amorphie.resource;

    public abstract class BaseRoleModule<TDTOModel, TDBModel, TValidator>
        : BaseBBTRouteRepository<TDTOModel, TDBModel, TValidator, ResourceDBContext, IBBTRepository<TDBModel, ResourceDBContext>>
        where TDTOModel : class, new()
        where TDBModel : EntityBase
        where TValidator : AbstractValidator<TDBModel>
    {
        protected BaseRoleModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => throw new NotImplementedException();

        public override string? UrlFragment => throw new NotImplementedException();

    }