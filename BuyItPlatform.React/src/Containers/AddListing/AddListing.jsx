import './AddListing.css'
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import { Loading } from '../../Components';
import Api from '../../Api/Api';
import { useParams } from 'react-router-dom';

import { useState } from 'react';
function AddListing() {
    const [newName, setNewName] = useState("");
    const [newDesc, setNewDesc] = useState("");
    const { slotIndex } = useParams();

    const categories = [
    ];
    const tags = [
    ];
    const subCategories = [
    ];
    const colors = [
    ];

    return (
        <main>
            <div className="holder">
                <input className="addlisting-name-input addlisting-text" autoComplete="off"
                    maxLength={15} type="text" value={newName}
                    onChange={(e) => { setNewName(e.target.value); }}
                    placeholder="name..."></input>
                <textarea className="addlisting-desc-input addlisting-text" autoComplete="off"
                    maxLength={250} type="text" value={newDesc}
                    onChange={(e) => { setNewDesc(e.target.value); }}
                    placeholder="desc..."></textarea>

                <div className="addlisting-images-holder">

                </div>
            </div>
        </main>
  );
}

export default AddListing;