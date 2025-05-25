import './UserOverview.css'
function UserOverview({ user }) {

    return (
        <main>
            <div className="holder">
                <label className="welcome-text fade-in">{user.userName}</label>
                <label className="welcome-text fade-in">{user.averageRating}</label>
                <label className="welcome-text fade-in">{user.numberOfRatings}</label>
                <label className="welcome-text fade-in">{user.phoneNumber}</label>
                <label className="welcome-text fade-in">{user.profileImgLink}</label>
            </div>
        </main>
    );
}

export default UserOverview;