import {useEffect, useState} from 'react';
import { GetOrderDetails, GetOrderDetailsCustomer, GetOrderDetailsSeller } from "../../services/OrderService";
import { useParams } from 'react-router-dom';
import OrderItem from './OrderItem';
import Card from 'react-bootstrap/Card';

function OrderDetails(){
    const {orderID} = useParams();
    const [order, setOrder] = useState([]);
    const [message, setMessage] = useState('');
    const userType = localStorage.getItem('userType');

    useEffect(()=>{
      const get = async() => {
        if(userType === 'ADMIN'){
          await GetOrderDetails(orderID).then((response) => {
            setOrder(response.data);
            setMessage('');
          }).catch((error) => {
            setOrder([]);
            setMessage(error.response.data);
          });
        }
        else if(userType === 'CUSTOMER'){
          await GetOrderDetailsCustomer(orderID).then((response) => {
            setOrder(response.data);
            setMessage('');
          }).catch((error) => {
            setOrder([]);
            setMessage(error.response.data);
          });
        }
        else if(userType === 'SELLER'){
          await GetOrderDetailsSeller(orderID).then((response) => {
            setOrder(response.data);
            setMessage('');
          }).catch((error) => {
            setOrder([]);
            setMessage(error.response.data);
          });
        }
      }
      get();
  }, [orderID, userType])

    return (
      <div style={{textAlign:"center"}}>
      <label>{message}</label>
      <Card>
          <Card.Body>
            <Card.Text style = {{fontWeight: 'bold'}}>{order.customerID}</Card.Text>
            <Card.Subtitle className="mb-2 text-muted">{order.deliveryAddress}</Card.Subtitle>
            {Object.keys(order).includes('timeOfOrder') && <Card.Text>Time of order: {order.timeOfOrder.replace('T', ' ')}</Card.Text>}
            {Object.keys(order).includes('deliveryTime') && <Card.Text>Delivery time: {order.deliveryTime.replace('T', ' ')}</Card.Text>}
            <Card.Text>Price: {order.price} RSD</Card.Text>
            {Object.keys(order).includes('products') && <Card.Text>Ordered items: {order.products.length}</Card.Text>}
            {order.orderStatus === 0 && <Card.Text>Order status: DELIEVERED</Card.Text>}
            {order.orderStatus === 1 && <Card.Text>Order status: IN PROGRESS</Card.Text>}
            {order.orderStatus === 2 && <Card.Text>Order status: CANCELED</Card.Text>}
          </Card.Body>
        </Card>
      <ul style={{display:"inline-block"}}>
        {Object.keys(order).includes('products') && order.products.map(product => (
          <OrderItem
            key = {product.productID}
            orderItem = {product}
          >
          </OrderItem>
        ))}
      </ul>
      </div>
  );
}

export default OrderDetails;