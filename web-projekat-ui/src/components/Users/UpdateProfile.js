import Form from 'react-bootstrap/Form';
import {useEffect, useState} from 'react';
import { GetUserDetails } from '../../services/UserService';
import { useParams } from 'react-router-dom';
import Button from 'react-bootstrap/esm/Button';
import { UpdateUserInfo } from '../../services/UserService';
import { useNavigate } from 'react-router-dom';
import { UpdateUserPicture } from '../../services/UserService';

function UpdateProfile(){
    const navigate = useNavigate();
    const {email} = useParams();
    const [user, setUser] = useState([]);
    const [message, setMessage] = useState('');

    const handleUpdate = async (event) =>{
        event.preventDefault();
        const data = {
            Email : email,
            Username : document.getElementById("username").value,
            FirstName: document.getElementById("firstName").value,
            LastName: document.getElementById("lastName").value,
            Address: document.getElementById("address").value,
            DateOfBirth: document.querySelector('input[type="date"]').value
        }
        if(data.Username === "")
            data.Username = user.username
        if(data.FirstName === "")
            data.FirstName = user.firstName
        if(data.LastName === "")
            data.LastName = user.lastName
        if(data.Address === "")
            data.Address = user.address
        if(data.DateOfBirth === "")
            data.DateOfBirth = user.dateOfBirth

        let info = {
            token: localStorage.getItem('token'),
            user: data
        }

        await UpdateUserInfo(info).then((response) => {
            navigate('/profile/' + email)
        }).catch((error) => {
            setMessage(error.response.data);
        })
    }

    const handlePictureUpdate = async () => {
        if(document.querySelector('#file').files.length === 0)
            return;
        console.log('a')
        var formData = new FormData();
        console.log(document.querySelector('#file').files[0])
        const image = document.querySelector('#file').files[0];
        formData.append("file", image);

        const data = {
            formData: formData,
            token: localStorage.getItem('token')
        }

        await UpdateUserPicture(data).then((response) => {
            navigate('/profile/' + email)
        }).catch((error) => {
            setMessage(error.response.data)
        })
    }

    useEffect(()=>{
        const get = async() => {
            let info = {
                email: email,
                token: localStorage.getItem('token')
            }
            await GetUserDetails(info).then((response) => {
                setUser(response.data);
                setMessage('');
            }).catch((error) => {
                setUser([]);
                setMessage(error.response.data);
            });
        }
        get();
    }, [email])

    return (
        <>
        <Form onSubmit={handleUpdate}>
            <Form.Group className="mb-3">
                <Form.Label>Select new profile picture</Form.Label>
                <br/>
                <input type="file" id="file" name="file"/>
            </Form.Group>
            <Button variant="primary" onClick={handlePictureUpdate}>Update picture</Button>
            <Form.Group className="mb-3">
                <Form.Label>Username:</Form.Label>
                <Form.Control id="username" placeholder={user.username}/>
            </Form.Group>
            <Form.Group className="mb-3">
                <Form.Label>First name:</Form.Label>
                <Form.Control id="firstName" placeholder={user.firstName}/>
            </Form.Group>
            <Form.Group className="mb-3">
                <Form.Label>Last name:</Form.Label>
                <Form.Control id="lastName" placeholder={user.lastName}/>
            </Form.Group>
            <Form.Group className="mb-3">
                <Form.Label>Address:</Form.Label>
                <Form.Control id="address" placeholder={user.address}/>
            </Form.Group>
            <Form.Group className="mb-3">
                <Form.Label>Date of birth:</Form.Label>
                <input id="dateofBirth" type="date" value={user.dateofBirth}/>
            </Form.Group>
            <Form.Text muted>{message}</Form.Text>
            <br/>
            <Button type="submit">Update profile</Button>
        </Form>
        </>
    );
}

export default UpdateProfile;