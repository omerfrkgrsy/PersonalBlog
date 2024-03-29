﻿namespace PersonalBlog.Core.Models;

public class PersonalBlogConfig
{
    #region Props
       
    public string PrivateKey { get; set; }
    public string RedisEndPoint { get; set; }
    public int RedisPort { get; set; }
    public string RabbitMqHostname { get; set; }
    public string RabbitMqUsername { get; set; }
    public string RabbitMqPassword { get; set; }
    public int RedisTimeout { get; set; }

    #endregion
    public PersonalBlogConfig()
    {
        PrivateKey = "generatedkey";
    }
}