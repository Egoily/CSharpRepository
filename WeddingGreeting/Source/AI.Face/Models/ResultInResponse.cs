using System.Collections.Generic;

namespace Baidu.AI.Face.Models
{
    public class FaceSearchResult
    {
        /// <summary>
        ///
        /// </summary>
        public string face_token { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<RecognitionInfo> user_list { get; set; }
    }

    public class FaceRegisterResult
    {
        /// <summary>
        ///
        /// </summary>
        public string face_token { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Location location { get; set; }
    }

    public class GetUserResult
    {
        /// <summary>
        ///
        /// </summary>
        public List<UserInfo> user_list { get; set; }
    }

    public class GetGroupsResult
    {
        /// <summary>
        ///
        /// </summary>
        public List<string> group_id_list { get; set; }
    }

    public class GetUsersResult
    {
        /// <summary>
        ///
        /// </summary>
        public List<string> user_id_list { get; set; }
    }
}