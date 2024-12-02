import { useContext, useState } from "react";
import { UserContext } from "../context/UserContext";
import { createPost } from "../functions/post";

const AddPostPopup = ({ isOpen, onClose }) => {
  const [postContent, setPostContent] = useState("");
  const [imageURL, setImageURL] = useState("");
  const { currentUser, handleChange } = useContext(UserContext);

  const handleSubmit = async (e) => {
    e.preventDefault();
    await createPost(currentUser.userId, postContent, imageURL);
    handleChange();
    onClose();
  };

  return (
    <>
      <div className={`add-post-overlay ${isOpen ? 'open' : ''}`}
        onClick={onClose}>
      </div>
      <div className={`add-post-popup ${isOpen ? 'open' : ''}`} onClick={(e) => e.stopPropagation()}>
        <form onSubmit={handleSubmit}>
          <h3>Create a Post</h3>
          <input
            type="text"
            placeholder="Enter Post Here"
            value={postContent}
            onChange={(e) => setPostContent(e.target.value)}
          />
          <input
            type="text"
            placeholder="Enter Image URL (optional)"
            value={imageURL}
            onChange={(e) => setImageURL(e.target.value)}
          />
          <button type="submit">Create Post</button>
          <button type="button" onClick={onClose} style={{ backgroundColor: "red" }}>
            Cancel
          </button>
        </form>
      </div>
    </>
  );
};

export default AddPostPopup;