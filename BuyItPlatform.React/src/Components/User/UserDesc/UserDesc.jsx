import './UserDesc.css'
import { Loading } from '../../../Components';

function UserDesc({editable, desc}) {
    return desc ?
    (
        <div className="user-description">
            { desc }
        </div>
    ):(
        <div className="user-description">
            <Loading />
        </div>
    );
}

export default UserDesc;