﻿namespace Domain.Configuration
{
    public class Secrets
    {
        public Secrets() 
        {
            FitExerciseDB = string.Empty;
            ClientSecret = string.Empty;
            PreSalt = string.Empty;
            PosSalt = string.Empty;
            S3Url = string.Empty;
        }

        public string FitExerciseDB { get; set; }
        public string ClientSecret { get; set; }
        public string PreSalt { get; set; }
        public string PosSalt { get; set; }
        public string S3Url {get; set;}
    }
}
