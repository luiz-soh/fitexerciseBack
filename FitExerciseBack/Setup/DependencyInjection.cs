using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.CreateExercise;
using Application.FitWorkout.Commands.EditExerciseData;
using Application.FitWorkout.Commands.EditExerciseMedia;
using Application.FitWorkout.Commands.GetExerciseById;
using Application.FitWorkout.Commands.GetExercises;
using Application.FitWorkout.Handlers;
using Application.FitWorkout.UseCase;
using Application.Gym.Commands.CreateGym;
using Application.Gym.Commands.LogIn;
using Application.Gym.Handlers;
using Application.Gym.UseCase;
using Application.S3.UseCase;
using Application.Token.UseCase;
using Application.User.Commands.AddUserEmail;
using Application.User.Commands.DeleteUser;
using Application.User.Commands.GetUserData;
using Application.User.Commands.GetUsersByGym;
using Application.User.Commands.SignIn;
using Application.User.Commands.SignUp;
using Application.User.Handlers;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Gym;
using Domain.DTOs.Token;
using Domain.DTOs.User;
using Domain.Entities.FitWorkout;
using Domain.Entities.Gym;
using Domain.Entities.Plan;
using Domain.Entities.User;
using Infra.Repository.FitWorkout;
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
            services.AddScoped<IFitWorkoutRepository, FitWorkoutRepository>();

            //Token
            services.AddScoped<ITokenUseCase, TokenUseCase>();

            //User
            services.AddScoped<IUserUseCase, UserUseCase>();
            services.AddTransient<IRequestHandler<AddUserEmailCommand, bool>, AddUserEmailHandler>();
            services.AddTransient<IRequestHandler<GetUserDataCommand, UserDto>, GetUserDataHandler>();
            services.AddTransient<IRequestHandler<GetUsersByGymCommand, List<UserDto>>, GetUsersByGymHandler>();
            services.AddTransient<IRequestHandler<SignInCommand, TokenDto>, SignInHandler>();
            services.AddTransient<IRequestHandler<SignUpCommand, bool>, SignUpHandler>();
            services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUserHandler>();

            //Gym
            services.AddScoped<IGymUseCase, GymUseCase>();
            services.AddTransient<IRequestHandler<CreateGymCommand, bool>, CreateGymHandler>();
            services.AddTransient<IRequestHandler<LogInCommand, GymTokenDto>, LogInHandler>();

            //FitWorkout
            services.AddScoped<IFitWorkoutUseCase, FitWorkoutUseCase>();
            services.AddTransient<IRequestHandler<GetExecisesCommand, List<ExerciseOutput>>, GetExercisesHandler>();
            services.AddTransient<IRequestHandler<GetExerciseByIdCommand, FullExerciseOutput>, GetExerciseByIdHandler>();
            services.AddTransient<IRequestHandler<CreateExerciseCommand, bool>, CreateExerciseHandler>();
            services.AddTransient<IRequestHandler<EditExerciseDataCommand, bool>, EditExerciseDataHandler>();
            services.AddTransient<IRequestHandler<EditExerciseMediaCommand, bool>, EditExerciseMediaHandler>();

            //S3
            services.AddScoped<IS3UseCase, S3UseCase>();
        }
    }
}
