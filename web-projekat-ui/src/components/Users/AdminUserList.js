import AdminUserView from "./AdminUserView";
import {useEffect, useState} from 'react';
import { GetCustomers, GetSellers } from "../../services/AdminService";

function AdminUserList(){
    const [users, setUsers] = useState([]);
    const [message, setMessage] = useState('');

    useEffect(()=>{
        const get = async() => {
            if(window.location.href.includes('customers')){
                await GetCustomers(localStorage.getItem('token')).then((response) => {
                    setUsers(response.data)
                    setMessage('')
                }).catch((error) => {
                    setMessage(error.response.data);
                })
            }
            else{
                await GetSellers(localStorage.getItem('token')).then((response) => {
                    setUsers(response.data)
                    setMessage('')
                }).catch((error) => {
                    setMessage(error.response.data);
                })
            }
        }
        get();
    }, [])

    return (
        <div style={{textAlign:"center"}}>
        <label>{message}</label>
        <ul style={{display:"inline-block"}}>
          {users.map(user => (
            <AdminUserView
              user = {user}
              key = {user.email}
            >
            </AdminUserView>
          ))}
        </ul>
        </div>
    );
}

export default AdminUserList;