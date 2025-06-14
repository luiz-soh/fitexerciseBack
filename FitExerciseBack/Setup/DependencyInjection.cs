﻿using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.CreateExercise;
using Application.FitWorkout.Commands.EditExerciseData;
using Application.FitWorkout.Commands.EditExerciseMedia;
using Application.FitWorkout.Commands.GetExerciseById;
using Application.FitWorkout.Commands.GetExercises;
using Application.FitWorkout.Commands.GetGymExercises;
using Application.FitWorkout.Handlers;
using Application.FitWorkout.UseCase;
using Application.GroupWorkout.Boundaries;
using Application.GroupWorkout.Commands;
using Application.GroupWorkout.Commands.CreateGroup;
using Application.GroupWorkout.Commands.DeleteGroupById;
using Application.GroupWorkout.Commands.GetGroupById;
using Application.GroupWorkout.Commands.UpdateGroup;
using Application.GroupWorkout.Handlers;
using Application.GroupWorkout.UseCase;
using Application.Gym.Commands.CreateGym;
using Application.Gym.Commands.LogIn;
using Application.Gym.Handlers;
using Application.Gym.UseCase;
using Application.Hiit.Boundaries;
using Application.Hiit.Commands;
using Application.Hiit.Commands.GetHiitSeriesById;
using Application.Hiit.Handlers;
using Application.Hiit.UseCase;
using Application.S3.UseCase;
using Application.Token.UseCase;
using Application.User.Boundaries.Output;
using Application.User.Commands.AddUserEmail;
using Application.User.Commands.DeleteUser;
using Application.User.Commands.ForgotPassword;
using Application.User.Commands.GetUserData;
using Application.User.Commands.GetUsersByGym;
using Application.User.Commands.RecoverPassword;
using Application.User.Commands.RefreshToken;
using Application.User.Commands.SignIn;
using Application.User.Commands.SignUp;
using Application.User.Commands.UpdateNextChange;
using Application.User.Handlers;
using Application.User.UseCase;
using Application.UserWorkout.Boundaries;
using Application.UserWorkout.Commands;
using Application.UserWorkout.Handlers;
using Application.UserWorkout.UseCase;
using Application.UserWorkout.v2.Boundaries;
using Application.UserWorkout.v2.Commands;
using Application.UserWorkout.v2.Handlers;
using Domain.Base.Communication;
using Domain.Base.Email;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Gym;
using Domain.DTOs.Token;
using Domain.DTOs.User;
using Domain.Entities.FitWorkout;
using Domain.Entities.GroupWorkout;
using Domain.Entities.Gym;
using Domain.Entities.Hiit;
using Domain.Entities.Plan;
using Domain.Entities.User;
using Domain.Entities.UserWorkout;
using Infra.Repository.FitWorkout;
using Infra.Repository.GroupWorkout;
using Infra.Repository.Gym;
using Infra.Repository.Hiit;
using Infra.Repository.Plan;
using Infra.Repository.User;
using Infra.Repository.UserWorkout;
using LaBarber.Infra.Services;
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
            services.AddScoped<IGroupWorkoutRepository, GroupWorkoutRepository>();
            services.AddScoped<IHiitRepository, HiitRepository>();
            services.AddScoped<IHiitSerieRepository, HiitSerieRepository>();
            services.AddScoped<IUserWorkoutRepository, UserWorkoutRepository>();

            //Token
            services.AddScoped<ITokenUseCase, TokenUseCase>();

            //User
            services.AddScoped<IUserUseCase, UserUseCase>();
            services.AddTransient<IRequestHandler<AddUserEmailCommand, bool>, AddUserEmailHandler>();
            services.AddTransient<IRequestHandler<GetUserDataCommand, UserDto>, GetUserDataHandler>();
            services.AddTransient<IRequestHandler<GetUsersByGymCommand, PaginatedUsersOutput>, GetUsersByGymHandler>();
            services.AddTransient<IRequestHandler<SignInCommand, TokenDto>, SignInHandler>();
            services.AddTransient<IRequestHandler<SignUpCommand, bool>, SignUpHandler>();
            services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUserHandler>();
            services.AddTransient<IRequestHandler<RefreshTokenCommand, TokenDto>, RefreshTokenHandler>();
            services.AddTransient<IRequestHandler<ForgotPasswordCommand, bool>, ForgotPasswordHandler>();
            services.AddTransient<IRequestHandler<RecoverPasswordCommand, bool>, RecoverPasswordHandler>();
            services.AddTransient<IRequestHandler<UpdateNextChangeCommand, bool>, UpdateNextChangeHandler>();

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
            services.AddTransient<IRequestHandler<GetGymExecisesCommand, PaginatedExercisesOutput>, GetGymExercisesHandler>();

            //GroupWorkout
            services.AddScoped<IGroupWorkoutUseCase, GroupWorkoutUseCase>();
            services.AddTransient<IRequestHandler<GetGroupsCommand, List<GroupWorkoutOutput>>, GetGroupsHandler>();
            services.AddTransient<IRequestHandler<CreateGroupCommand, bool>, CreateGroupHandler>();
            services.AddTransient<IRequestHandler<UpdateGroupCommand, bool>, UpdateGroupHandler>();
            services.AddTransient<IRequestHandler<GetGroupByIdCommand, GroupWorkoutOutput>, GetGroupByIdHandler>();
            services.AddTransient<IRequestHandler<DeleteGroupByIdCommand, bool>, DeleteGroupByIdHandler>();


            //hiit
            services.AddScoped<IHiitUseCase, HiitUseCase>();
            services.AddTransient<IRequestHandler<GetHiitByCategoryIdCommand, List<HiitOutput>>, GetHiitByCategoryIdHandler>();
            services.AddTransient<IRequestHandler<GetHiitSeriesByIdCommand, List<HiitSerieOutput>>, GetHiitSeriesByIdHandler>();

            //UserWorkout
            services.AddScoped<IUserWorkoutUseCase, UserWorkoutUseCase>();
            services.AddTransient<IRequestHandler<AddUserWorkoutCommandOld, bool>, AddUserWorkoutHandlerOld>();
            services.AddTransient<IRequestHandler<GetUserExercisesCommand, List<UserExerciseOutput>>, GetUserExercisesHandler>();
            services.AddTransient<IRequestHandler<DeleteUserWorkoutCommand, bool>, DeleteUserWorkoutHandler>();
            services.AddTransient<IRequestHandler<ChangeUserWorkoutPositionCommand, bool>, ChangeUserWorkoutPositionHandler>();
            services.AddTransient<IRequestHandler<UpdateUserWorkoutCommand, bool>, UpdateUserWorkoutHandler>();
            //V2
            services.AddTransient<IRequestHandler<AddUserWorkoutCommand, bool>, AddUserWorkoutHandler>();
            services.AddTransient<IRequestHandler<UpdateUserWorkoutsCommand, bool>, UpdateUserWorkoutsHandler>();
            services.AddTransient<IRequestHandler<GetUserWorkoutsCommand, List<DynamoUserWorkoutOutput>>, GetUserWorkoutsHandler>();
            services.AddTransient<IRequestHandler<AddCheckInWorkoutCommand, bool>, AddCheckInWorkoutHandler>();
            services.AddTransient<IRequestHandler<ListCheckInsCommand, List<CheckInWorkoutOutput>>, ListCheckInsHandler>();

            //S3
            services.AddScoped<IS3UseCase, S3UseCase>();

            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}
