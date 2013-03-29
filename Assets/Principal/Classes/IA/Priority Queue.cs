using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PriorityQueue<T> {
	
		private SortedList<int, List<T>> _list;	
	
		public PriorityQueue(){
			_list = new SortedList<int, List<T>>();

		}
		
		public void addElement(T element, int priority){
			if (!_list.ContainsKey(priority)){
				_list.Add(priority,new List<T>());	
			}
		
			((List<T>)_list[priority]).Add(element);
			
		}
		
		public T getFirst(){
			if (!isEmpty()){
				return (_list.Values[0][0]);
			}
			return default(T);
	
		}
	
		public void removeFirst(){
			if (!isEmpty()){
				((List<T>)_list.Values[0]).RemoveAt(0);
				if (_list.Values[0].Count == 0){
					_list.RemoveAt(0);
				}
			}
			
		}
	
		public void print(){
			if (!isEmpty()){
				for (int  i = 0; i < _list.Count;i++){
					for (int z = 0; z < _list.Values[i].Count ; z++)
					{
						Debug.Log("Clave: " + _list.Keys[i] + 
						" Elemento: " + _list.Values[i][z].ToString());
					}	
				}
			}
		}
	
		public bool isEmpty(){
			return (_list.Count == 0);
		}
	
		public T contains(T other){
			if (!isEmpty()){
				for (int  i = 0; i < _list.Count;i++){
					for (int z = 0; z < _list.Values[i].Count ; z++)
					{
						if (Equals(other,_list.Values[i][z])){
							return _list.Values[i][z];
						}
					}	
				}
			}
			return default(T);
		}
	
		public void delete(T other){
			if (!isEmpty()){
				for (int  i = 0; i < _list.Count;i++){
					for (int z = 0; z < _list.Values[i].Count ; z++)
					{
						if (Equals(other,_list.Values[i][z])){
							((List<T>)_list.Values[i]).RemoveAt(z);
							if (_list.Values[i].Count == 0){
								_list.RemoveAt(i);
							}
							break;
						}
					}	
				}
			}
		}
}