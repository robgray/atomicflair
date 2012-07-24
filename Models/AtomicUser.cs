using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace atomicflair.Models
{
	public class AtomicUser
	{
        public AtomicUser(int userId)
        {
            UserId = userId;
        }

		/// <summary>
		/// Url to the image for this user
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// The user id
		/// </summary>
		public int UserId { get; private set; }

		/// <summary>
		/// The handle used on Atomic.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The Atomic rank based on post count
		/// </summary>
		public string Rank { get; set; }

		/// <summary>
		/// The Special Rank. 
		/// One of Hero, Superhero, Mod, God, Atomican, Lurker
		/// </summary>
		private string _specialRank;
		public string SpecialRank
		{
			get { return _specialRank; }
			set
			{
				_specialRank = value;
				if (_specialRank == "God") IsGod = true;
				if (_specialRank == "Mod") IsMod = true;
				if (_specialRank.ToLower().IndexOf("hero") >= 0) IsHeroic = true;
			}
		}

		public bool IsHeroic { get; private set; }

		public bool IsMod { get; private set; }

		public bool IsGod { get; private set; }

        public string ProfileUrl 
        {
            get { return string.Format("http://forums.atomicmpc.com.au/index.php?showuser={0}", UserId);  }
        } 
	}
}
