using System;
using System.Collections.Generic;
using System.Text;

namespace SportsHub.Model
{
    public class AppSettings
    {

        public string AWSProfileName { get; set; }
        public string AWSRegion { get; set; }
        public string AccessKeyID { get; set; }
        public string SecretAccessKey { get; set; }
        public string LogPath { get; set; }
        public string InprogressRevertTime { get; set; }
        public string TokenSecretKey { get; set; }
        public string Environment { get; set; }
        public string LonestarBaseURL { get; set; }
        public string LonestarCitationURL { get; set; }
        public string LonestarSearchBaseURL { get; set; }
        public string AssetID { get; set; }
        public string AssetSecrete { get; set; }
        public string[] Roles { get; set; }
        public double authenticationExpirationMinutes { get; set; }
        public string JWTAudiance { get; set; }
        public string JWTIssuer { get; set; }
        public string JWTSecKey { get; set; }
        public string AWSRDSSecretName { get; set; }
        public string AWSAppSecretsName { get; set; }
        public bool IsAWSSecretEnabled { get; set; }
        public bool IsLNAuthentictionEnabled { get; set; }
        public string AVURL { get; set; }
        public string APIHostURL { get; set; }
    }
}
