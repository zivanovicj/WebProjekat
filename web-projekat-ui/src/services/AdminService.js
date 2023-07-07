import axios from "axios";

export const GetCustomers = async (token) => {
    return await axios.get('https://localhost:44365/api/Admins/customers', {
        headers:{
            'Authorization': 'Bearer ' + token
        }
    })
}

export const GetSellers = async (token) => {
    return await axios.get('https://localhost:44365/api/Admins/sellers', {
        headers:{
            'Authorization': 'Bearer ' + token
        }
    })
}

export const VerifySeller = async (data) => {
    return await axios.post('https://localhost:44365/api/Admins/verify/' + data.email, null, {
        headers: {
            'Authorization': 'Bearer ' + data.token
        }
    })
}

export const RejectSeller = async (data) => {
    return await axios.post('https://localhost:44365/api/Admins/reject/' + data.email, null, {
        headers: {
            'Authorization': 'Bearer ' + data.token
        }
    })
}