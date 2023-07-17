import axios from "axios";

export const GetOrders = async (token) => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Orders`, {
        headers:{
            'Authorization': 'Bearer ' + token
        }
    });
}

export const GetOrderDetails = async (orderID) => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Orders/admin/${orderID}`, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetDelieveredOrders = async () => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Orders/delivered`, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetOrderDetailsCustomer = async (orderID) => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Orders/${orderID}`, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetCanceledOrders = async () => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Orders/canceled`, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetDeliveredSeller = async () => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Orders/seliveredSeller`, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetOrderDetailsSeller = async (orderID) => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Orders/seller/${orderID}`, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetCanceledSeller = async () => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Orders/canceledSeller`, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const CreateOrder = async (data) => {
    return await axios.post(`${process.env.REACT_APP_API_URL}/Orders/newOrder`, data, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetPending = async () => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Orders/pending`, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const RemoveOrder = async (orderID) => {
    return await axios.delete(`${process.env.REACT_APP_API_URL}/Orders/${orderID}`, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const GetPendingSeller = async () => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Orders/pendingSeller`, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}