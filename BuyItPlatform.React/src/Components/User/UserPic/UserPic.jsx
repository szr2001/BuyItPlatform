import './UserPic.css'
import { Loading } from '../../../Components';

function UserPic({editable, picLink}) {
    return picLink ?
        (
            <img className="user-pic" src={picLink}>
            </img>
        ) : (
            <div className="user-pic">
                <Loading />
            </div>
        );
}

export default UserPic;