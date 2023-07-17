import axios from "axios";

export const GetCustomers = async (token) => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Admins/customers`, {
        headers:{
            'Authorization': 'Bearer ' + token
        }
    })
}

export const GetSellers = async (token) => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Admins/sellers`, {
        headers:{
            'Authorization': 'Bearer ' + token
        }
    })
}

export const VerifySeller = async (data) => {
    return await axios.post(`${process.env.REACT_APP_API_URL}/Admins/verify/${data.email}`, null, {
        headers: {
            'Authorization': 'Bearer ' + data.token
        }
    })
}

export const RejectSeller = async (data) => {
    return await axios.post(`${process.env.REACT_APP_API_URL}/Admins/reject/${data.email}`, null, {
        headers: {
            'Authorization': 'Bearer ' + data.token
        }
    })
}