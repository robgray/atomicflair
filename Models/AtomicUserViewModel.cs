namespace flair.Models
{
    public class AtomicUserViewModel
    {
        /// <summary>
        /// Url to the image for this user
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The user id
        /// </summary>
        public int UserId { get; set; }

        public string ProfileUrl { get; set; }

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
        public string SpecialRank { get; set; }

        public string SpecialRankColor { get; set; }

        public static AtomicUserViewModel Create(AtomicUser user)
        {
            var userViewModel = new AtomicUserViewModel
            {
                ImageUrl = user.ImageUrl,
                UserId = user.UserId,
                Name = user.Name,
                Rank = user.Rank,
                SpecialRank = user.SpecialRank,
                ProfileUrl = user.ProfileUrl
            };

            return userViewModel;
        }
    }
}