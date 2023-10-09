using Application.Token.UseCase;
using Application.User.Commands.AddUserEmail;
using Application.User.Commands.GetUserData;
using Application.User.Commands.GetUsersByGym;
using Application.User.Commands.SignIn;
using Application.User.Handlers;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Token;
using Domain.DTOs.User;
using Domain.Entities.Gym;
using Domain.Entities.Plan;
using Domain.Entities.User;
using Infra.Repository.Gym;
using Infra.Repository.Plan;
using Infra.Repository.User;
using MediatR;

namespace FitExerciseBack.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterService(this IServiceCollection services)
        {
            //Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //DomainNotification
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGymRepository, GymRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();

            //Token
            services.AddScoped<ITokenUseCase, TokenUseCase>();

            //User
            services.AddScoped<IUserUseCase, UserUseCase>();

            services.AddTransient<IRequestHandler<AddUserEmailCommand, bool>, AddUserEmailHandler>();
            services.AddTransient<IRequestHandler<GetUserDataCommand, UserDto>, GetUserDataHandler>();
            services.AddTransient<IRequestHandler<GetUsersByGymCommand, List<UserDto>>, GetUsersByGymHandler>();
            services.AddTransient<IRequestHandler<SignInCommand, TokenDto>, SignInHandler>();
        }
    }
}
