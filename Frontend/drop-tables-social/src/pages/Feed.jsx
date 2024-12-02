import { useContext } from "react";
import { useState, useEffect } from "react";
import { UserContext } from "../context/UserContext";
import { getPosts } from "../functions/post";
import Post from "../components/Post";
import { getUsers } from "../functions/user"

function Feed() {
  const { currentUser } = useContext(UserContext);
  const [posts, setPosts] = useState([]);
  const [users, setUsers] = useState([]);

  useEffect(() => {
    const fetchPosts = async () => {
      const userIds = currentUser.following.map((user) => user.userID);
      const posts = await getPosts(userIds);
      const users = await getUsers(userIds);
      console.log(userIds)
      const filteredPosts = posts.filter((post) => post.userId !== currentUser.userId);
      setPosts(filteredPosts);
      setUsers(users);
    };

    fetchPosts();
  }, [currentUser]);

  return (
    <div>
        {posts.map((post) => {
          const user = users?.find((u) => u.userId === post.userId);
          return user ? (
            <div key={post.postId} id="post-list">
              <Post postId = {post.postId} user = {user} content ={post.content}/>
            </div>
          ) : null;
        })}
    </div>
  );
}

export default Feed;