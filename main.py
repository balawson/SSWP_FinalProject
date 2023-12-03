from fastapi import FastAPI, HTTPException, Depends
from sqlalchemy import create_engine, Column, Integer, String
from sqlalchemy.orm import declarative_base, sessionmaker, Session
from typing import List, Optional

SQLALCHEMY_DATABASE_URL = "sqlite:///C:/Users/blake/Cards.db"
engine = create_engine(SQLALCHEMY_DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

# FastAPI setup
app = FastAPI()

# Models
Base = declarative_base()

class Card(Base):
    __tablename__ = "BlackjackCards"

    id = Column(Integer, primary_key=True, index=True)
    suit = Column(String, index=True)
    value = Column(String)

# Dependency to get the database session
def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

# API Endpoints

from random import shuffle

@app.post("/shuffle-deck/")
def shuffle_deck(db: Session = Depends(get_db)):
    try:
        # Attempt to delete all existing cards from the database
        db.query(Card).delete()

        # Define the suits and values for a standard deck of cards
        suits = ["Spades", "Clubs", "Diamonds", "Hearts"]
        values = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace"]

        # Create a list to store shuffled cards for 6 decks
        decks = 6
        shuffled_cards = [{"suit": suit, "value": value} for _ in range(decks) for suit in suits for value in values]
        shuffle(shuffled_cards)

        # Add the shuffled cards to the database
        for card in shuffled_cards:
            db_card = Card(suit=card["suit"], value=card["value"])
            db.add(db_card)

        # Commit the changes to the database
        db.commit()

        # Return a success message
        return {"message": f"{decks} decks shuffled successfully"}

    except Exception as e:
        # If an error occurs, print the error message and raise an HTTP 500 Internal Server Error
        print(f"An error occurred during shuffle_deck: {e}")
        raise HTTPException(status_code=500, detail="Internal Server Error")



@app.get("/deal-card/")
def deal_card(db: Session = Depends(get_db)):
    try:
        # Attempt to retrieve the first card from the database
        card = db.query(Card).order_by(Card.id).first()

        # If a card is found, delete it from the database and commit the changes
        if card:
            db.delete(card)
            db.commit()
            return {"card": {"suit": card.suit, "value": card.value}, "message": "Card dealt successfully"}
        else:
            # If no cards are left in the deck, trigger a shuffle and notify the start of a new shoe
            shuffle_deck(db)
            return {"message": "Start of a new shoe. Deck shuffled successfully"}

    except Exception as e:
        # If an error occurs, print the error message and raise an HTTP 500 Internal Server Error
        print(f"An error occurred during deal_card: {e}")
        raise HTTPException(status_code=500, detail="Internal Server Error")

def shuffle_deck(db: Session):
    try:
        # Attempt to delete all existing cards from the database
        db.query(Card).delete()

        # Define the suits and values for a deck of cards
        suits = ["Spades", "Clubs", "Diamonds", "Hearts"]
        values = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace"]

        # Create a list to store shuffled cards for 6 decks
        decks = 6
        shuffled_cards = [{"suit": suit, "value": value} for _ in range(decks) for suit in suits for value in values]
        shuffle(shuffled_cards)

        # Add the shuffled cards to the database
        for card in shuffled_cards:
            db_card = Card(suit=card["suit"], value=card["value"])
            db.add(db_card)

        # Commit the changes to the database
        db.commit()

    except Exception as e:
        # If an error occurs during the shuffle, print the error message and raise an HTTP 500 Internal Server Error
        print(f"An error occurred during shuffle_deck: {e}")
        raise HTTPException(status_code=500, detail="Internal Server Error")


# Clear Deck implemented for testing purposes
@app.post("/clear-deck/")
def clear_deck(db: Session = Depends(get_db)):
    try:
        # Attempt to delete all existing cards from the database
        db.query(Card).delete()
        db.commit()
        return {"message": "Deck cleared successfully"}

    except Exception as e:
        # If an error occurs during the operation, print the error message and raise an HTTP 500 Internal Server Error
        print(f"An error occurred during clear_deck: {e}")
        raise HTTPException(status_code=500, detail="Internal Server Error")
