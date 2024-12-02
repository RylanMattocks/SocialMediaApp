import { useEffect, useState } from "react";
import { getUsernameById } from "../functions/user";

const PostTile = ( post ) => {
  const [ postUsername, setPostUsername ] = useState("loading...");
  const [ postImage, setPostImage ] = useState("");

  useEffect(() => {
    if(post?.userId) {
      getUsernameById(post?.userId).then((username) => {
        setPostUsername(username);
      });
    }
    if(post?.imageURL) {
      setPostImage(post?.imageURL);
    }
  }, [post?.userId, post?.imageURL]);

  return (
    <div>
      <h3>{postUsername}</h3>
      {postImage && (
        <img src={postImage} alt="Post Image" />
      )}
      <p>{post?.content}</p>
    </div>
  )
}

export default PostTile;