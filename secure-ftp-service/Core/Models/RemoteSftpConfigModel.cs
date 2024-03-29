﻿using secure_ftp_service.Helpers;

namespace secure_ftp_service.Core.Models
{
    /// <summary>
    /// <para>It defines all the important attributes, which are needed to connect with sftp or ftp server and helps user to do various operations,</para> 
    /// <para>as connect, disconnect, upload, download files and so on.</para>
    /// </summary>
    public class RemoteSftpConfigModel
    {
        public string Type { get; set; } = ConstantSupplier.REMOTE_TYPE_SFTP;
        public string HostName { get; set; } = ConstantSupplier.REMOTE_HOST_NAME;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 2222;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string LocalPathDirectory { get; set; } = string.Empty;
        public string ServerPathBaseDirectory { get; set; } = string.Empty;
        public string ServerPathDirectory { get; set; } = string.Empty;
    }
}
