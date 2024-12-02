import './Styles/Profile.css'
import './Styles/ProfilePage.css'
import './Styles/ProfileTab.css'

function App() {

  return (
    <>
      <div className='content'>
        <h1>Drop Tables</h1>
        <ul>
          <li><a href="/api/users">Users</a></li>
          <li><a href="/api/posts">Posts</a></li>
          <li><a href="/api/follows">Follows</a></li>
        </ul>
      </div>
    </>
  )
}

export default App
