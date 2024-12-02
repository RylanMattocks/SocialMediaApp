import { useContext, useState } from "react";
import { UserContext } from "../context/UserContext";

const Profile = () => {
  const { currentUser } = useContext(UserContext);
  const [currentTab, setCurrentTab] = useState("followers");
  const [isListVisible, setIsListVisible] = useState(true);

  const handleTabClick = (tab) => {
    setCurrentTab(tab);
    setIsListVisible(!isListVisible);
  };

  return (
    <div className="profile-info">
      <img className="profile-avatar" src={currentUser?.profileImageUrl} alt="User Avatar"/>
      <div className="profile-details">
        <h2>{currentUser.username}</h2>
        <h3>Posts: {currentUser.posts?.length}</h3>
        <h3>Followers: {currentUser.followers?.length}</h3>
        <h3>Following: {currentUser.following?.length}</h3>
        {/**
        <p>
          <button onClick={() => handleTabClick("followers")}>
            Followers: {currentUser.followers?.length}
          </button>
        </p>
        <p>
          <button onClick={() => handleTabClick("following")}>
            Following: {currentUser.following?.length}
          </button>
        </p>

        {isListVisible && currentTab === "followers" ? (
          <div>
            <h3>Followers:</h3>
            <ul>
              {currentUser.followers?.map((follower, index) => (
                <li key={index}>{follower.username}</li>
              ))}
            </ul>
          </div>
        ) : null}
        {isListVisible && currentTab === "following" ? (
          <div>
            <h3>Following:</h3>
            <ul>
              {currentUser.following?.map((following, index) => (
                <li key={index}>{following.username}</li>
              ))}
            </ul>
          </div>
        ) : null}
        */}
      </div>
      <div className="profile-about">
        <h3>About Me</h3>
        <p>{currentUser.about}</p>
        <p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vestibulum tortor quam, feugiat vitae, ultricies eget, tempor sit amet, ante. Donec eu libero sit amet quam egestas semper. Aenean ultricies mi vitae est. Mauris placerat eleifend leo.</p>
      </div>
    </div>
  );
};

export default Profile;