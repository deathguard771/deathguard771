function []=fun()
    a=[1,0,0;0,1,1;0,0,0];
    d=[0;0;10];
    C=rref([a d]);
    [n,m]=size(C);
    x=C(:,m)
endfunction
