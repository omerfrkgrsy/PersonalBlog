﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBlog.Core;

public class BaseEntity:IEntity
{
    private DateTime dateTime;
    [NotMapped]
    public DateTime UsedTime { get { this.dateTime = DateTime.Now; return dateTime; } set { } }
}