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

export const GetDelieveredOrders = async () => {
    return await axios.get('https://localhost:44365/api/Orders/delivered', {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetOrderDetailsCustomer = async (orderID) => {
    return await axios.get('https://localhost:44365/api/Orders/' + orderID, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetCanceledOrders = async () => {
    return await axios.get('https://localhost:44365/api/Orders/canceled', {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetDeliveredSeller = async () => {
    return await axios.get('https://localhost:44365/api/Orders/deliveredSeller', {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetOrderDetailsSeller = async (orderID) => {
    return await axios.get('https://localhost:44365/api/Orders/seller/' + orderID, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetCanceledSeller = async () => {
    return await axios.get('https://localhost:44365/api/Orders/canceledSeller', {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const CreateOrder = async (data) => {
    return await axios.post('https://localhost:44365/api/Orders/newOrder', data, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}