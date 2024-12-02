import { useContext, useState, useEffect } from "react"
import Profile from "../components/Profile";
import ProfileTab from "../components/ProfileTab";
import Post from "../components/Post";
import { UserContext } from "../context/UserContext";
import { getUsers } from "../functions/user"
import "../Styles/ProfilePage.css"
import "../Styles/Home.css"

const ProfilePage = () => {
    const [tab, setTab] = useState("posts");
    const { currentUser } = useContext(UserContext);
    const [users, setUsers] = useState([]);

    useEffect(() => {
        const fetchPosts = async () => {
          const userIds = currentUser.likes.map((user) => user.userId);
          const users = await getUsers(userIds);
          setUsers(users);
        };
    
        fetchPosts();
    }, [currentUser]);

    const handleTabChange = (selectedTab) => {
        setTab(selectedTab);
    }
    return (
        <div className="profile-page">
            <Profile />
            <ProfileTab onTabChange={handleTabChange}/>
            <div>
                {tab === "posts" &&
                    currentUser?.posts?.map((post, index) => (
                        <div key={post.postId}>
                            <Post postId = {post.postId} user = {currentUser} content ={post.content}/>
                        </div>
                    ))
                }
                {tab === "likes" && 
                     currentUser.likes.map((post) => {
                        const user = users?.find((u) => u.userId === post.userId);
                        return user ? (
                          <div key={post.postId} id="post-list">
                            <Post postId = {post.postId} user = {user} content ={post.content}/>
                          </div>
                        ) : null;
                      })
                }
            </div>
        </div>
    )
}

export default ProfilePage;