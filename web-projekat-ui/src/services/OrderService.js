import axios from "axios";

export const GetOrders = async (token) => {
    return await axios.get('https://localhost:44365/api/Orders', {
        headers:{
            'Authorization': 'Bearer ' + token
        }
    });
}

export const GetOrderDetails = async (orderID) => {
    return await axios.get('https://localhost:44365/api/Orders/admin/' + orderID, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}