import Order from "./Order";
import {useEffect, useState} from 'react';
import { GetDeliveredSeller } from "../../services/OrderService";

function DeliveredOrdersSeller(){
    const [orders, setOrders] = useState([]);
    const [message, setMessage] = useState('');

    useEffect(() => {
        const get = async () => {
            await GetDeliveredSeller().then((response) => {
                setOrders(response.data);
                setMessage('');
            }).catch((error) => {
                setMessage(error.response.data)
            })
        }

        get();
    }, [])

    return (
        <div style={{textAlign:"center"}}>
        <label>{message}</label>
        <ul style={{display:"inline-block"}}>
          {orders.map(order => (
            <Order
              key = {order.orderID}
              order = {order}
            >
            </Order>
          ))}
        </ul>
        </div>
    );
}

export default DeliveredOrdersSeller;