import { useState } from "react"

const ProfileTab = ({onTabChange}) => {
    const [activeTab, setActiveTab] = useState("posts");

    const handleTabClick = (tab) => {
        setActiveTab(tab);
        onTabChange(tab);
    }

    return (
        <div className="profile-tab">
            <button className={activeTab === "posts" ? "active" : ""} onClick={() => handleTabClick("posts")}>
                Posts
            </button>
            <button className={activeTab === "likes" ? "active" : ""} onClick={() => handleTabClick("likes")}>
                Likes
            </button>
        </div>
    )
}

export default ProfileTab;