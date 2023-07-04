import axios from 'axios';

export const LogIn = async (loginform) =>{
    return await axios.post('https://localhost:44365/api/Users/login', loginform);
}