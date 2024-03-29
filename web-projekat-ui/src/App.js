import { useEffect, useState } from 'react';
import './App.css';
import Navigation from './components/Navigation';
import { GetProducts } from './services/ProductService';
import ProductList from './components/Products/ProductList.js';
import {Routes, Route} from 'react-router-dom';
import ProductDetails from './components/Products/ProductDetails';
import LogInForm from './components/Users/LogInForm';
import UserProfile from './components/Users/UserProfile';
import UpdateProfile from './components/Users/UpdateProfile';
import AdminUserList from './components/Users/AdminUserList';
import RegisterUser from './components/Users/RegisterUser';
import OrderList from './components/Orders/OrderList';
import OrderDetails from './components/Orders/OrderDetails';
import DelieveredOrdersList from './components/Orders/DelieveredOrdersList';
import CanceledOrdersList from './components/Orders/CanceledOrdersList';
import DeliveredOrdersSeller from './components/Orders/DeliveredOrdersSeller';
import CanceledOrdersSeller from './components/Orders/CanceledOrdersSeller';
import SellerProducts from './components/Products/SellerProducts';
import NewProduct from './components/Products/NewProduct';
import ModifyProduct from './components/Products/ModifyProduct';
import { CartProvider } from './store/CartContext';
import PendingOrdersList from './components/Orders/PendingOrdersList';
import PendingOrdersSeller from './components/Orders/PendingOrdersSeller';
import ChangePasswordForm from './components/Users/ChangePasswordForm';

function App() {
  const [products, setProducts] = useState([]);
  
  useEffect(()=>{
    const get = async() => {
      await GetProducts().then((response) => {
        setProducts(response.data);
      }).catch((error) => {
      });
    }
    get();
  }, [])

  return (
    <>
    <CartProvider>
    <Navigation></Navigation>
    
    <Routes>
      <Route path="/" element={<ProductList products={products}/>}></Route>
      <Route path="/details/:id" element={<ProductDetails/>}></Route>
      <Route path="/login" element={<LogInForm/>}></Route>
      <Route path="/register" element={<RegisterUser/>}></Route>
      <Route path='/profile/:email' element={<UserProfile/>}></Route>
      <Route path='/updateProfile/:email' element={<UpdateProfile/>}></Route>
      <Route path='/customers' element={<AdminUserList/>}></Route>
      <Route path='/sellers' element={<AdminUserList/>}></Route>
      <Route path='/allOrders' element={<OrderList/>}></Route>
      <Route path='/orderDetails/:orderID' element={<OrderDetails/>}></Route>
      <Route path='/delivered' element={<DelieveredOrdersList/>}></Route>
      <Route path='/canceled' element={<CanceledOrdersList/>}></Route>
      <Route path='/deliveredSeller' element={<DeliveredOrdersSeller/>}></Route>
      <Route path='/canceledSeller' element={<CanceledOrdersSeller/>}></Route>
      <Route path='/myProducts/:email' element={<SellerProducts/>}></Route>
      <Route path='/newProduct' element={<NewProduct/>}></Route>
      <Route path='/modifyProduct/:productID' element={<ModifyProduct/>}></Route>
      <Route path='/pending' element={<PendingOrdersList/>}></Route>
      <Route path='/pendingSeller' element={<PendingOrdersSeller/>}></Route>
      <Route path='/changePassword' element={<ChangePasswordForm/>}></Route>
    </Routes>
    </CartProvider>
    </>
  );
}

export default App;
