﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
			string url = "http://localhost:57230/api/TodoItems";

			try
			{
				var task = url.GetJsonAsync<TodoItem[]>();
				TodoItem[] todoItems  = task.Result;
			}
			catch (Exception ex)
			{
			}
        }
	}
}
