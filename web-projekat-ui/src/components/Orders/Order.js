import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import { RemoveOrder } from '../../services/OrderService';
import {useState} from 'react';
import { useNavigate } from 'react-router-dom';
import Countdown from 'react-countdown';

function Order(props){
    const order = props.order
    const [message, setMessage] = useState('');
    const navigate = useNavigate();
    const diff = Date.parse(order.deliveryTime) - Date.now()
    const date = new Date(Date.now() + diff);
    const userType = localStorage.getItem('userType');

    const handleCancelOrder = async () => {
      await RemoveOrder(order.orderID).then((response) => {
        navigate(0);
      }).catch((error) => {
        setMessage(error.response.data);
      })
    }

    const renderer = ({ days, hours, minutes, seconds }) => {
      return <Card.Text>Delivery countdown: {days} day(s), {hours}:{minutes}:{seconds}</Card.Text>;
    };

    return (
        <Card style={{ width: '20rem' }}>
          <Card.Body>
            <Card.Text style = {{fontWeight: 'bold'}}>{order.customerID}</Card.Text>
            <Card.Subtitle className="mb-2 text-muted">{order.deliveryAddress}</Card.Subtitle>
            <Card.Text>Time of order: {order.timeOfOrder.replace('T', ' ')}</Card.Text>
            <Card.Text>Delivery time: {order.deliveryTime.replace('T', ' ')}</Card.Text>
            <Card.Text>Price: {order.price} RSD</Card.Text>
            {order.orderStatus === 0 && <Card.Text>Order status: DELIEVERED</Card.Text>}
            {order.orderStatus === 1 && <Card.Text>Order status: IN PROGRESS</Card.Text>}
            {order.orderStatus === 2 && <Card.Text>Order status: CANCELED</Card.Text>}
            {(userType === 'CUSTOMER' || userType === 'SELLER') && order.orderStatus === 1 && <Countdown date = {date} renderer={renderer}></Countdown>}
            <Button href={'/orderDetails/' + order.orderID} variant="primary">Details</Button>
            <Card.Text>{message}</Card.Text>
            {userType === 'CUSTOMER' && window.location.href.indexOf("pending") !== -1 &&<Button onClick={handleCancelOrder} variant="primary">Cancel order</Button>}
          </Card.Body>
        </Card>
    );
}

export default Order;