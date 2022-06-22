using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secure_ftp_service.Helpers
{
    /// <summary>
    /// This class contains all the constant values.
    /// </summary>
    public class ConstantSupplier
    {
        /// <summary>
        /// Common Constants
        /// </summary>
        public const string APP_CONFIG_SFTP_DB_CONN_NAME = "SftpFileConnection";
        public const string SWAGGER_INVENTORY_SERVICE_DOC_VERSION_NAME = "v1";
        public const string SWAGGER_INVENTORY_SERVICE_DOC_TITLE = "Inventory Service";
        public const string SWAGGER_INVENTORY_SERVICE_DOC_END_POINT = "/swagger/v1/swagger.json";
        public const string SWAGGER_INVENTORY_SERVICE_DOC_END_POINT_NAME = "Inventory Service v1";
        public const string COMMON_API_ROUTE_ATTRIBUTE = "api/[controller]";
        public const string EMPTY_STRING = "";
        public const string STRING_YES = "Y";
        public const string STRING_NO = "N";

        /// <summary>
        /// Common Constant Messages
        /// </summary>
        public const string CANNOT_BE_EMPTY_MSG = " cannot be empty";
        public const string BAD_REQUEST_MSG = "Bad Request";
        public const string INTERNAL_SERVER_ERROR_MSG = "Internal Server Error";
        public const string SUCCESS_MSG = "Success";
        public const string ACCEPTED_MSG = "Accepted";

        /// <summary>
        /// Cor Policy
        /// </summary>
        public const string CORS_POLICY_NAME = "CorsPolicyAllowAll";

        /// <summary>
        /// Serilog Log Constants
        /// </summary>
        public const string LOG_WRITE_PATH_MAIN = "d:\\Personal Dev Work\\GitRepo\\Backend API Test\\logs\\inventory-log-.txt";
        public const string LOG_OUTPUT_TEMPLATE_MAIN = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
        public const string LOG_INFO_WEB_HOST_START_MSG = "Starting web host";
        public const string LOG_ERROR_WEB_HOST_TERMINATE_MSG = "Host terminated unexpectedly";
        public const string LOG_INFO_APP_START_MSG = "Application is starting";
        public const string LOG_FATAL_APP_FAILED_MSG = "Application is starting";

        /// <summary>
        /// Name Constants
        /// </summary>
        public const string APP_SETTINGS_FILE_NAME = "appsettings.json";
        public const string CONFIG_BIND_KEY = "AppSettings:DatabaseConnection";

        /// <summary>
        /// Service Start Page
        /// </summary>
        public const string LOG_INFO_APPEND_LINE_FIRST = "**********************************************************************";
        public const string LOG_INFO_APPEND_LINE_SECOND_GATEWAY = "**                    Secure FTP Service                            **";
        public const string LOG_INFO_APPEND_LINE_THIRD_VERSION = "**                       [Version 1.0.0]                            **";
        public const string LOG_INFO_APPEND_LINE_FOURTH_COPYRIGHT = "**        ©2022-2023 Sample Inc. All rights reserved                **";
        public const string LOG_INFO_APPEND_LINE_END = "**********************************************************************";

        /// <summary>
        /// Sftp Service Constants
        /// </summary>
        public const string REMOTE_TYPE_SFTP = "SFTP";
        public const string REMOTE_TYPE_FTP = "FTP";
        public const string REMOTE_HOST_NAME = "test.rebex.net";
        public const string EXCEPTION_MSG = "Exception Message: ";
        public const string NEW_LINE = "\n";
        public const string EXCEPTION_INNER_MSG = "Inner Excep. Message: ";
        public const string LIST_SFTP_DATA_LIST_RETRIVE_SUCCESS_MSG = "sftp server data list retrieved successfully";
        public const string LIST_SFTP_DATA_LIST_RETRIVE_FAILED_MSG = "sftp server data list not found";
        public const string LIST_SFTP_DATA_RETRIVE_SUCCESS_MSG = "sftp server data retrieved successfully";
        public const string LIST_SFTP_DATA_RETRIVE_FAILED_MSG = "sftp server data not found";
        public const string SFTP_DATA_SAVE_SUCCESS_MSG = "sftp server data save successfully";
        public const string SFTP_DATA_SAVE_FAILED_MSG = "sftp server data saving failed";
        public const string SFTP_DATA_FOUND_SUCCESS_MSG = "sftp server data found";
        public const string SFTP_DATA_FOUND_FAILED_MSG = "sftp server data not found";

        /// <summary>
        /// Sftp Controller Constants
        /// </summary>
        public const string ATTRIBUTE_ROUTE = "api/[controller]";
        public const string DOWNLOAD_FILES_ROUTE_NAME = "downloadsftpfiles";
        public const string DOWNLOAD_SUCCESS_MSG = "download successful!";
        public const string DOWNLOAD_FAILED_MSG = "download failed";
        public const string DOWNLOAD_FILES_NOT_FOUND_MSG = "downloaded files not found";
        public const string DB_DESTINATION_PATH_NOT_FOUND = "stored files destination is unknown!";
        public const string NO_NEW_FILES = "No new files found";
        public const string NO_FILES_FOUND_SERVER_PATH = "No new files found in server path";
        public const string CONTROLLER_ACTION_ERROR_MSG = "Something went wrong in the {0} and exception message is {1}";
        public const string BACKGROUND_WORK_ERROR_MSG = "Something went wrong in the {0} and exception message is {1}";
        public const string BASE_PATH = "D:\\Personal Work\\Personal Projects\\GitRepo\\sftp-service\\rebex_tiny_sftp_server\\data";

        /// <summary>
        /// Api related all constants from start to finish.
        /// </summary>
        public const string HTTP_CLIENT_LOGICAL_NAME = "SFTPclient";
        public const string API_GET_DOWNLOAD_URL = "/api/SftpService/downloadsftpfiles";
        public const string HTTP_HEADERS_CONTENT_TYPE_NAME = "Accept";
        public const string HTTP_HEADERS_CONTENT_TYPE_VALUE = "application/json";
        public const string CORSS_POLICY_NAME = "AllowRedirectOrigin";

    }
}
