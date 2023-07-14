import React, { createContext, useState } from 'react';

export const CartContext = createContext();

export const CartProvider = ({ children }) => {
  const [cartItems, setCartItems] = useState([]);

  const addToCart = (item) => {
    const existingID = cartItems.findIndex(product => product.productID === item.productID);
    if(existingID === -1)
      setCartItems((prevItems) => [...prevItems, item]);
    else{
      const cartItem = cartItems[existingID];
      const newCartItems = cartItems.map(element => {
        if(element.productID === cartItem.productID){
          return {...element, quantity: element.quantity + 1}
        }
        return element;
      });
      setCartItems(newCartItems);
    }
  };

  const removeFromCart = (productID) => {
    const existingID = cartItems.findIndex(product => product.productID === productID);
    if(cartItems[existingID].quantity === 1)
      setCartItems((prevItems) => prevItems.filter((item) => item.productID !== productID));
    else{
      const cartItem = cartItems[existingID];
      const newCartItems = cartItems.map(element => {
        if(element.productID === cartItem.productID){
          return {...element, quantity: element.quantity - 1}
        }
        return element;
      });
      setCartItems(newCartItems);
    }
  };

  const clearCart = () => {
    setCartItems([]);
  };

  return (
    <CartContext.Provider
      value={{
        cartItems,
        addToCart,
        removeFromCart,
        clearCart,
      }}
    >
      {children}
    </CartContext.Provider>
  );
};
