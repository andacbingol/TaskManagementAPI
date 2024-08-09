namespace TaskManagementAPI.WebAPI.Configurations
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public static class Projects
        {
            public const string GetProjects = Base + "/users/{userId}" + "/projects";
            public const string GetProjectById = Base + "/users/{userId}" + "/projects/{Id}";
            public const string CreateProject = Base + "/users/{userId}" + "/Projects";
            public const string DeleteProject = Base + "/users/{userId}" + "/projects/{Id}";
            public const string UpdateProject = Base + "/users/{userId:Guid}" + "/projects/{Id:Guid}";
        }
        public static class Tasks
        {
            public const string GetTasks = Base + "/users/{userId}" + "/projects/{projectId}" + "/tasks";
            public const string GetTaskById = Base + "/users/{userId}" + "/projects/{projectId}" + "/tasks/{Id}";
            public const string CreateTask = Base + "/users/{userId}" + "/projects/{projectId}" + "/tasks";
            public const string DeleteTask = Base + "/users/{userId}" + "/projects/{projectId}" + "/tasks/{Id}";
            public const string UpdateTask = Base + "/users/{userId}" + "/projects/{projectId}" + "/tasks/{Id}";
        }
        public static class Users
        {
            public const string GetUsers = Base + "/users";
            public const string GetUserById = Base + "/users/{Id}";
            public const string CreateUser = Base + "/users";
            public const string DeleteUser = Base + "/users/{Id}";
            public const string UpdateUser = Base + "/users/{Id}";
            public const string UpdatePassword = Base + "/users/updatePassword";
        }
        public static class Authentication
        {
            public const string Login = Base + "/auth/login";
            public const string GoogleLogin = Base + "/auth/google-login";
            public const string FacebookLogin = Base + "/auth/facebook-login";
            public const string RefreshTokenLogin = Base + "/auth/refresh-token-login";
            public const string GenerateConfirmEmailToken = Base + "/auth/generate-confirm-email-token";
            public const string ConfirmEmail = Base + "/auth/confirm-email";
            public const string PasswordReset = Base + "/auth/generate-password-reset-token";
            public const string ConfirmPasswordReset = Base + "/auth/confirm-password-reset";
        }
        public static class Roles
        {
            public const string GetRoles = Base + "/roles";
            public const string GetRoleById = Base + "/roles/{Id}";
            public const string CreateRole = Base + "/roles";
            public const string DeleteRole = Base + "/roles/{Id}";
            public const string UpdateRole = Base + "/roles/{Id}";
        }

        public static class ApplicationServices
        {
            public const string GetAuthorizeDefinitionEndpoints = Base + "/ApplicationServices";
        }

        public static class AuthorizationEndpoints
        {
            public const string GetRolesToEndpoint = "/AuthorizationEndpoints/GetRolesToEndpoint";
            public const string AssignRoleToEndpoint = "/AuthorizationEndpoints/AssignRoleToEndpoint";
        }
    }
}
