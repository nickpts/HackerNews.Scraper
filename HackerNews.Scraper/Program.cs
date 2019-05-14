using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using HackerNews.Reader;
using Microsoft.Extensions.Configuration;

namespace HackerNews.Skills
{
    class Program
    {
		public static Dictionary<string, int> results = new Dictionary<string, int>() { };
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var reader = new PostReader(0, CommentLevel.FirstLevel);
			var post = reader.GetById(new int[] { 19797594 }, new CancellationToken()).First();

			var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();

			var values = config["skills"].Split(',');

			var filteredPosts = GetPostBySkill(post, values).ToList();
        }

		public static IEnumerable<Post> GetPostBySkill(Post post, string[] values)
		{
			foreach (var comment in post.Comments)
			{
				if (comment.Text != null && comment.Text.ToLower().Contains(values.First()))
				{
					yield return comment;
				}
			}
		}
    }
}
