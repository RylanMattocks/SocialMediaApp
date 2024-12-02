import { useContext, useEffect, useState } from "react";
import { UserContext } from "../context/UserContext";
import FollowButton from "./FollowButton";
import UnfollowButton from "./UnfollowButton";
import LikeButton from "./LikeButton";
import DislikeButton from "./DislikeButton";
import './Post.css';
import { getPost } from "../functions/post";

export default function Post({postId, user, content}){
    const { currentUser } = useContext(UserContext);
    const [ post, setPost ] = useState(null);
    const [ change, setChange ] = useState(false);
    const followingIds = currentUser.following.map(user => user.userID);
    const likedIds = currentUser.likes.map(like => like.postId);
    
    const onChange = () => {
        setChange(prev => !prev)
    }

    useEffect(() => {
        const fetchPost = async () => {
          const post = await getPost(postId);
          setPost(post);
        };
        fetchPost();
      }, [postId, change]);

    return(
        <div className="post">
            <h3 className="post-header">
                <div className="post-user-info">
                    <p>{user.username}</p>
                    <img src={user.profileImageUrl}></img>
                </div>
                {followingIds.includes(user.userId) ? (<UnfollowButton userId = {user.userId}/>):(<FollowButton userId = {user.userId}/>)}
            </h3>
            <div className="post-content">
                <h2>{content}</h2>
            </div>
            <div className="post-interactions">
            <p>Likes: {post?.likes.length}</p>
                {likedIds.includes(postId) ? 
                (<><DislikeButton postId = {postId} onChange = {onChange}/><div className="heart"></div></>) : 
                (<><LikeButton postId = {postId} onChange = {onChange}/><div className="heart" style = {{borderImage: "radial-gradient(black 69%, #0000 70%) 84.5%/50%"}}></div></>)}
            </div>
            
        </div>
    )
}